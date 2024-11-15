/*  ---------------------------------------------------
    Template Name: Ogani
    Description:  Ogani eCommerce  HTML Template
    Author: Colorlib
    Author URI: https://colorlib.com
    Version: 1.0
    Created: Colorlib
---------------------------------------------------------  */

'use strict';

(function ($) {

    /*------------------
        Preloader
    --------------------*/
    $(window).on('load', function () {
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");

        /*------------------
            Gallery filter
        --------------------*/
        $('.featured__controls li').on('click', function () {
            $('.featured__controls li').removeClass('active');
            $(this).addClass('active');
        });
        if ($('.featured__filter').length > 0) {
            var containerEl = document.querySelector('.featured__filter');
            var mixer = mixitup(containerEl);
        }
    });

    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    //Humberger Menu
    $(".humberger__open").on('click', function () {
        $(".humberger__menu__wrapper").addClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").addClass("active");
        $("body").addClass("over_hid");
    });

    $(".humberger__menu__overlay").on('click', function () {
        $(".humberger__menu__wrapper").removeClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").removeClass("active");
        $("body").removeClass("over_hid");
    });

    /*------------------
        Navigation
    --------------------*/
    $(".mobile-menu").slicknav({
        prependTo: '#mobile-menu-wrap',
        allowParentLinks: true
    });

    /*-----------------------
        Categories Slider
    ------------------------*/
    $(".categories__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 4,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            0: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 3,
            },

            992: {
                items: 4,
            }
        }
    });


    $('.hero__categories__all').on('click', function () {
        $('.hero__categories ul').slideToggle(400);
    });

    /*--------------------------
        Latest Product Slider
    ----------------------------*/
    $(".latest-product__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 1,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*-----------------------------
        Product Discount Slider
    -------------------------------*/
    $(".product__discount__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 3,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            320: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 2,
            },

            992: {
                items: 3,
            }
        }
    });

    /*---------------------------------
        Product Details Pic Slider
    ----------------------------------*/
    $(".product__details__pic__slider").owlCarousel({
        loop: true,
        margin: 20,
        items: 4,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*-----------------------
        Price Range Slider
    ------------------------ */
    var rangeSlider = $(".price-range"),
        minamount = $("#minamount"),
        maxamount = $("#maxamount"),
        minPrice = rangeSlider.data('min'),
        maxPrice = rangeSlider.data('max');
    rangeSlider.slider({
        range: true,
        min: minPrice,
        max: maxPrice,
        values: [minPrice, maxPrice],
        slide: function (event, ui) {
            minamount.val('$' + ui.values[0]);
            maxamount.val('$' + ui.values[1]);
        }
    });
    minamount.val('$' + rangeSlider.slider("values", 0));
    maxamount.val('$' + rangeSlider.slider("values", 1));

    /*------------------
        Single Product
    --------------------*/
    $('.product__details__pic__slider img').on('click', function () {

        var imgurl = $(this).data('imgbigurl');
        var bigImg = $('.product__details__pic__item--large').attr('src');
        if (imgurl != bigImg) {
            $('.product__details__pic__item--large').attr({
                src: imgurl
            });
        }
    });

    /*-------------------
        Quantity change
    --------------------- */
    var proQty = $('.pro-qty');
    proQty.prepend('<span class="dec qtybtn">-</span>');
    proQty.append('<span class="inc qtybtn">+</span>');
    $(document).ready(function () {
        $('#userIcon').click(function (event) {
            event.preventDefault();
            $('#menuLink').toggle();
        });

        $(document).click(function (event) {
            if (!$(event.target).closest('#userIcon, #menuLink').length) {
                $('#menuLink').hide();
            }
        });

        $('.quantity .pro-qty span').on('click', function () {
            const button = $(this);
            const input = button.closest('.quantity').find('input');
            const oldValue = parseFloat(input.val());
            const index = input.data("cart-detail-index");
            const price = parseFloat(input.data("cart-detail-price"));
            const discountRate = parseFloat(input.data("cart-detail-discount") / 100);
            const id = input.data("cart-detail-id");
            const quantityBook = input.data("book-quantity");
            let newVal = oldValue;
            let change = 0;

            if (button.hasClass('dec') && oldValue > 1) {
                newVal = oldValue - 1;
                change = -1;
            } else if (!button.hasClass('dec')) {
                newVal = oldValue + 1;
                change = 1;
            }
            if (newVal > quantityBook) {
                $.toast({
                    heading: 'Error',
                    text: 'The quantity you selected exceeds the available stock limit.',
                    position: 'top-right',
                    icon: 'error'
                });
                return;
            }
            input.val(newVal);
            $(`#CartDetailDtos_${index}__Quantity`).val(newVal);
            const priceTotalElement = $(`td[data-cart-detail-id='${id}']`);
            priceTotalElement.text(formatCurrency(price * newVal));

            const totalPriceElement = $("span[data-cart-total-price]");
            const totalDiscountElement = $("span[data-cart-total-discount]");
            let totalPrice = parseFloat(totalPriceElement.attr("data-cart-total-price"));
            let totalDiscount = parseFloat(totalDiscountElement.attr("data-cart-total-discount"));

            if (change !== 0) {
                totalPrice += change * price;
                totalDiscount += change * price * discountRate;
            }

            totalPriceElement.text(formatCurrency(totalPrice)).attr("data-cart-total-price", totalPrice);
            totalDiscountElement.text(`- ${formatCurrency(totalDiscount)}`).attr("data-cart-total-discount", totalDiscount);

            $("#total").text(formatCurrency(totalPrice - totalDiscount));
        });
        function formatCurrency(value) {
            return new Intl.NumberFormat('en-US', {
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            }).format(value);
        }

        $('.btnAddToCartHomepage').click(function (event) {
            event.preventDefault();

            if (!isLogin()) {
                $.toast({
                    heading: 'Error',
                    text: 'You need to log in to your account',
                    position: 'top-right',
                    icon: 'error'
                })
                return;
            }
            const isDeleted = $(this).attr('data-book-isDeleted');
            if (isDeleted === "true" || isDeleted === "True") {
                $.toast({
                    heading: 'Error',
                    text: 'This product is no longer available.',
                    position: 'top-right',
                    icon: 'error'
                });
                return;
            }
            const bookId = $(this).attr('data-book-id');
            const token = $("meta[name='_csrf']").attr("content");
            const header = $("meta[name='_csrf_header']").attr("content");

            $.ajax({
                url: `${window.location.origin}/api/CartApi/add-book-to-cart`,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader(header, token);
                },
                type: "POST",
                data: JSON.stringify({ Quantity: 1, BookId: bookId }),
                contentType: "application/json",

                success: function (response) {
                    console.log(response)
                    const sum = +response;
                    //update cart
                    $("#sumCart").text(sum)
                    //show message
                    $.toast({
                        heading: 'Cart',
                        text: 'Product added to cart successfully',
                        position: 'top-right',

                    })

                },
                error: function (response) {
                    alert("có lỗi xảy ra :v")
                    console.log("error: ", response);
                }

            });
        });

        $('.btnAddToCartDetail').click(function (event) {
            event.preventDefault();
            if (!isLogin()) {
                $.toast({
                    heading: 'Error',
                    text: 'You need to log in to your account',
                    position: 'top-right',
                    icon: 'error'
                })
                return;
            }
            const isDeleted = $(this).attr('data-book-isDeleted');
            if (isDeleted === "true" || isDeleted === "True") {
                $.toast({
                    heading: 'Error',
                    text: 'This product is no longer available.',
                    position: 'top-right',
                    icon: 'error'
                });
                return;
            }
            const bookId = $(this).attr('data-book-id');
            const token = $("meta[name='_csrf']").attr("content");
            const header = $("meta[name='_csrf_header']").attr("content");
            const quantity = $("#CartDetailDtos_0__Quantity").val();
            $.ajax({
                url: `${window.location.origin}/api/CartApi/add-book-to-cart`,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader(header, token);
                },
                type: "POST",
                data: JSON.stringify({ Quantity: quantity, BookId: bookId }),
                contentType: "application/json",

                success: function (response) {
                    const sum = +response;
                    $("#sumCart").text(sum)
                    $.toast({
                        heading: 'Cart',
                        text: 'Product added to cart successfully',
                        position: 'top-right',
                    })
                },
                error: function (response) {
                    alert("có lỗi xảy ra :v")
                    console.log("error: ", response);
                }

            });
        });

        function isLogin() {
            const navElement = $("#navbarCheck");
            const childLogin = navElement.find('.check-login');
            if (childLogin.length > 0) {
                return false;
            }
            return true;
        }

        // Search
        const searchInput = document.getElementById('searchInput');
        const searchResultsContainer = document.getElementById('searchResultsContainer');
        const overlay = document.getElementById('overlay');

        searchInput.addEventListener('input', toggleBoxInput);
        searchInput.addEventListener('focus', toggleBoxFocus);

        document.addEventListener('click', function (e) {
            if (
                !searchInput.contains(e.target) &&
                !searchResultsContainer.contains(e.target)
            ) {
                searchResultsContainer.style.display = 'none';
                overlay.style.display = 'none';
            }
        });

        function showHideBox(query) {
            if (query !== '') {
                searchResultsContainer.style.display = 'block';
                overlay.style.display = 'block';
            } else {
                searchResultsContainer.style.display = 'none';
                overlay.style.display = 'none';
            }
        } 
        function toggleBoxFocus() {
            const query = searchInput.value.trim();
            showHideBox(query);
            return query;
        }
        function toggleBoxInput() {
            const query = toggleBoxFocus();
            if (query !== '') {
                searchResultsContainer.innerHTML = `
                            <div class="search-advanced">
                                <i class="fa fa-search"></i>
                                <p>Advanced search for: <strong>${query}</strong></p>
                                </div>`;

                $.ajax({
                    url: '/api/filter/searchNav',
                    method: 'GET',
                    data: { searchVal: query },
                    success: function (response) {
                        console.log('check')
                        response.books.forEach((item) => {
                            searchResultsContainer.innerHTML += `
                                <a href="/Item/GetBookDetail/${item.id}" target="_blank">
                                    <div class="search-suggestion-item">
                                        <img src="${item.image}" alt="${item.name}" class="book-image" />
                                        <div class="book-details">
                                            <span class="book-title">${item.name}</span>
                                            <span class="book-author">Author: ${item.author}</span>
                                            <span class="book-price">Price: ${formatCurrency(item.price)} đ</span>
                                        </div>
                                    </div>
                                </a>`;
                        });
                    },
                    error: function (error) {
                        console.error('Error fetching books:', error);
                    }
                });
            }
        }
        function adjustContainerWidth() {
            const searchForm = document.querySelector('.hero__search__form');
            const searchResultsContainer = document.getElementById('searchResultsContainer');

            if (searchForm && searchResultsContainer) {
                searchResultsContainer.style.width = `${searchForm.offsetWidth}px`;
                searchResultsContainer.style.left = `${searchForm.offsetLeft}px`;
            }
        }
        window.addEventListener('resize', adjustContainerWidth);
    });
})(jQuery);