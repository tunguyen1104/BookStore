using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public BookService(IUnitOfWork unitOfWork, ISessionService sessionService, IUserService userService, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _sessionService = sessionService;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Book?> GetByIdAsync(long id)
        {
            return await _unitOfWork.Books.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _unitOfWork.Books.GetAllAsync();

        }
        public int getTotal()
        {
            return _unitOfWork.Books.count();
        }
        public async Task<IEnumerable<HomePageBookDto>> GetBooksForHomepageAsync()
        {
            var newArrivalBookIds = await _unitOfWork.Books.GetNewArrivalBookIdsAsync();
            var bestSellerBookIds = await _unitOfWork.Books.GetMostBuyBookIdsAsync();

            IEnumerable<BookDto> books = findAll();

            var homePageBooks = books.Select(book => new HomePageBookDto
            {
                BookDto = book,
                IsNewArrival = newArrivalBookIds.Contains(book.Id),
                BestSeller = bestSellerBookIds.Contains(book.Id)
            });

            return homePageBooks;
        }

        public IEnumerable<BookDto> findAll()
        {
            var books = _unitOfWork.Books.GetAllAsync().Result;
            var bookList = books.Select(book => new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                DetailDesc = book.DetailDesc,
                Factory = book.Factory,
                Image = book.Image,
                Price = book.Price,
                Quantity = book.Quantity,
                ShortDesc = book.ShortDesc,
                Sold = book.Sold,
                Discount = book.Discount,
                IsDeleted = book.IsDeleted,
                Categories = book.Categories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                }).ToList()
            });
            return bookList;
        }

        public (IEnumerable<BookDto> Books, int TotalCount) Find(string search, int page, int pageSize)
        {
            if (page < 1) page = 1;
            IQueryable<Book> booksQuery = _unitOfWork.Books.GetAll().OrderBy(b => b.Id).Where(b => !b.IsDeleted.HasValue || !b.IsDeleted.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                booksQuery = booksQuery.Where(book => (book.Name.Contains(search) || book.Author.Contains(search)));
            }

            var totalCount = booksQuery.Count();

            // Apply paging
            var books = booksQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map to BookDto
            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                DetailDesc = book.DetailDesc,
                Factory = book.Factory,
                Image = _fileService.GetImageUrl(book.Image),
                Price = book.Price,
                Quantity = book.Quantity,
                ShortDesc = book.ShortDesc,
                Sold = book.Sold,
            });

            return (bookDtos, totalCount);
        }

        public IEnumerable<CategoryDto> GetAllBookCategories()
        {
            return _unitOfWork.Categories.GetAll().Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<CartSummaryDto?> HandleGetCartPageAsync()
        {
            User currentUser = _userService.GetCurrentUser();
            if (currentUser != null)
            {
                Cart? cart = await _unitOfWork.Carts.FetchByUserIdAsync(currentUser.Id);
                List<CartDetailDto> cartDetailDtos = cart == null
                        ? new List<CartDetailDto>()
                        : _mapper.Map<List<CartDetailDto>>(cart.CartDetails.ToList());
                double totalPrice = 0;
                double totalDiscount = 0;
                foreach (var cartDetailDto in cartDetailDtos)
                {
                    totalPrice += (double)(cartDetailDto.BookPrice * cartDetailDto.Quantity);
                    totalDiscount += (double)(cartDetailDto.BookPrice * (cartDetailDto.BookDiscount / 100) * cartDetailDto.Quantity);
                }

                return new CartSummaryDto(totalPrice, totalDiscount, cartDetailDtos);
            }
            return null;
        }
        public async Task AddBookToCartAsync(long bookId, long quantity)
        {
            User user = _userService.GetCurrentUser();
            if (user != null)
            {
                Cart? cart = await _unitOfWork.Carts.FetchByUserIdAsync(user.Id);
                if (cart == null)
                {
                    cart = new Cart { UserId = user.Id, Sum = 0 };
                    await _unitOfWork.Carts.AddAsync(cart);
                    await _unitOfWork.CompleteAsync();
                }

                Book? book = await _unitOfWork.Books.GetByIdAsync(bookId);
                if (book != null)
                {
                    var oldCartDetail = await _unitOfWork.CartDetails.GetCartDetailByCartAndBookAsync(cart.Id, bookId);

                    if (oldCartDetail != null)
                    {
                        oldCartDetail.Quantity += quantity;
                        _unitOfWork.CartDetails.Update(oldCartDetail);
                    }
                    else
                    {
                        var newCartDetail = new CartDetail
                        {
                            Cart = cart,
                            Book = book,
                            Quantity = quantity
                        };
                        await _unitOfWork.CartDetails.AddAsync(newCartDetail);
                        int sum = cart.Sum + 1;
                        _sessionService.UpdateCartSum(sum);
                        cart.Sum = sum;
                        _unitOfWork.Carts.Update(cart);
                    }
                }
                await _unitOfWork.CompleteAsync();
            }
        }
        public async Task HandleRemoveCartDetail(long cartDetailId)
        {
            var cartDetail = await _unitOfWork.CartDetails.GetByIdAsync(cartDetailId)
                     ?? throw new InvalidOperationException("CartDetail not exit.");

            var currentCart = await _unitOfWork.Carts.GetByIdAsync(cartDetail.CartId)
                             ?? throw new InvalidOperationException("Cart not exit.");

            _unitOfWork.CartDetails.Delete(cartDetail);

            if (currentCart.Sum > 1)
            {
                currentCart.Sum--;
                _sessionService.UpdateCartSum(currentCart.Sum);
                _unitOfWork.Carts.Update(currentCart);
            }
            else
            {
                _unitOfWork.Carts.Delete(currentCart);
                _sessionService.UpdateCartSum(0);
            }

            await _unitOfWork.CompleteAsync();
        }
        public async Task HandleUpdateCartBeforeCheckout(List<CartDetailDto> cartDetailDtos)
        {
            if (cartDetailDtos == null)
            {
                throw new ArgumentNullException(nameof(cartDetailDtos), "CartDetailDtos cannot be null.");
            }
            foreach (CartDetailDto cartDetailDto in cartDetailDtos)
            {
                CartDetail? cartDetail = await _unitOfWork.CartDetails.GetByIdAsync(cartDetailDto.Id);
                if (cartDetail != null)
                {
                    cartDetail.Quantity = cartDetailDto.Quantity;
                    _unitOfWork.CartDetails.Update(cartDetail);
                }
            }
            await _unitOfWork.CompleteAsync();
        }
        public async Task<CheckoutDto?> GetUserCheckoutDataAsync()
        {
            var userId = _userService.GetCurrentUser().Id;
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            return user == null ? null : new CheckoutDto
            {
                ReceivedName = user.FullName ?? string.Empty,
                ReceivedPhone = user.Phone ?? string.Empty,
                ReceivedAddress = user.Address ?? string.Empty
            };
        }
        public async Task HandleAddOrderAndOrderDetail(string receivedName, string receivedPhone,
            string receivedAddress, string orderNotes)
        {
            long userId = _userService.GetCurrentUser().Id;
            User? currentUser = await _unitOfWork.Users.GetByIdAsync(userId);
            if (currentUser == null) { throw new ArgumentNullException(nameof(currentUser), "User cannot be null."); }
            Cart? cart = await _unitOfWork.Carts.FetchByUserIdAsync(currentUser.Id);
            if (cart != null)
            {
                List<CartDetail> cartDetails = cart.CartDetails.ToList();
                if (cartDetails != null)
                {
                    // create new order
                    var order = new Order
                    {
                        UserId = currentUser.Id,
                        User = currentUser,
                        ReceivedName = receivedName,
                        ReceivedPhone = receivedPhone,
                        ReceivedAddress = receivedAddress,
                        OrderNotes = orderNotes,
                        Status = "PENDING",
                        TotalPrice = cartDetails.Sum(cd => cd.Book.Price * cd.Quantity)
                    };

                    await _unitOfWork.Orders.AddAsync(order);

                    foreach (var cartDetail in cartDetails)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Order = order,
                            Book = cartDetail.Book,
                            Quantity = cartDetail.Quantity
                        };
                        await _unitOfWork.OrderDetails.AddAsync(orderDetail);

                        // Update book stock and sales count
                        cartDetail.Book.Quantity -= cartDetail.Quantity;
                        cartDetail.Book.Sold += cartDetail.Quantity;
                    }

                    // delete cart
                    foreach (CartDetail cartDetail in cartDetails)
                    {
                        _unitOfWork.CartDetails.Delete(cartDetail);
                    }
                    _unitOfWork.Carts.Delete(cart);
                    await _unitOfWork.CompleteAsync();
                    _sessionService.UpdateCartSum(0);
                }
            }
        }

        public async Task<int> CreateNewBookAsync(BookDto bookdto)
        {
            List<Category> categories = new List<Category>();

            foreach (var id in bookdto.SelectedCategoryIds)
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category != null)
                {
                    categories.Add(category);
                }
            }
            Book book = new Book
            {
                Name = bookdto.Name,
                Author = bookdto.Author,
                DetailDesc = bookdto.DetailDesc,
                Categories = categories,
                Factory = bookdto.Factory,
                IsDeleted = false,
                Quantity = bookdto.Quantity,
                Price = bookdto.Price,
                Image = bookdto.Image,
                ShortDesc = bookdto.ShortDesc,
                Sold = 0,
            };
            await _unitOfWork.Books.AddAsync(book);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteBookAsync(long id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book != null)
            {
                book.IsDeleted = true;
                _unitOfWork.Books.Update(book);
                return await _unitOfWork.CompleteAsync() > 0;
            }
            return false;
        }

        public async Task<bool> UpdateBookAsync(CreateOrUpdateBookRequest bookdto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookdto.Id);
            if (book != null)
            {
                _mapper.Map(bookdto, book);
                if (bookdto.SelectedCategoryIds.Any())
                {
                    book.Categories.Clear();

                    foreach (var id in bookdto.SelectedCategoryIds)
                    {
                        var category = await _unitOfWork.Categories.GetByIdAsync(id);
                        if (category != null)
                        {
                            book.Categories.Add(category);
                        }
                    }
                }
                _unitOfWork.Books.Update(book);
                return await _unitOfWork.CompleteAsync() > 0;
            }
            return false;
        }

        public async Task<List<BookDto>> GetRelatedBooksAsync(Book book)
        {
            var relatedBooks = await _unitOfWork.Books.GetRelatedBooksAsync(book);
            return _mapper.Map<List<BookDto>>(relatedBooks.ToList());
        }
    }
}
