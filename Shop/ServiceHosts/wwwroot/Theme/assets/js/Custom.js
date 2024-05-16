var CookieName = "Cart-Item";
function AddToCart(id, name, price, picture, slug) {
    debugger;
    let products = $.cookie(CookieName);

    if (products === undefined)
        products = [];
    else {
        try {
            products = JSON.parse(products);
        }
        catch {
            $.removeCookie(CookieName);
            products = [];
        }
    }

    const count = $("#productCount").val();
    const CurrentProduct = products.find(x => x.Id == id);

    if (CurrentProduct === undefined) {
        const product = {
            Id: id,
            Name: name,
            Price: price,
            Picture: picture,
            Count: count,
            Slug: slug
        }

        products.push(product);
    }
    else {
        CurrentProduct.Count = parseInt(CurrentProduct.Count) + parseInt(count);
    }

    $.cookie(CookieName, JSON.stringify(products), { expires: 2, path: "/" });
    UpdateCart();
}

function UpdateCart() {
    let CartItem = $.cookie(CookieName);
    let Products = JSON.parse(CartItem);

    $("#CartCounter").text(Products.length);

    $("#CartItems").html('');

    let Currency = new Intl.NumberFormat();
    Products.forEach(x => {
        const product = `<div class="cart-items-wrapper ps-scroll">
                            <div class="single-cart-item">
                                <a href="javascript:void(0)" class="remove-icon" onclick="RemoveCartItem('${x.id}')">
                                    <i class="ion-android-close"></i>
                                </a>
                                <div class="image">
                                    <a href="single-product.html">
                                        <img src="ProductPictures/${x.Picture}"
                                             class="img-fluid" alt="">
                                    </a>
                                </div>
                                <div class="content">
                                    <p class="product-title">
                                        <a href="single-product.html">${x.Name}</a>
                                    </p>
                                    <p class="count">${x.Count} عدد</p>
                                    <p class="price">${Currency.format(parseInt(x.Price) * parseInt(x.Count))} تومان</p>
                                </div>
                            </div>
                         </div>`;

        $("#CartItems").append(product);
    });
}

function RemoveCartItem(id) {
    debugger;
    let products = $.cookie(CookieName);
    products = JSON.parse(products);

    let ItemIndex = parseInt(products.findIndex(x => x.Id == id));

    products.splice(ItemIndex, 1);
    $.cookie(CookieName, JSON.stringify(products), { expires: 2, path: "/" });

    UpdateCart();
}

function UpdateTotalPrice(id, totalPriceId, count) {
    debugger;
    var coockie = $.cookie(CookieName);
    var items = JSON.parse(coockie);

    var ItemForIncreaseCount = parseInt(items.findIndex(x => x.Id == id));

    items[ItemForIncreaseCount].Count = count;
    var totalPrice = parseInt(count) * parseInt(items[ItemForIncreaseCount].Price);

    var CurrencyFormat = new Intl.NumberFormat();
    var totalPriceToShow = `${CurrencyFormat.format(totalPrice)} تومان`;
    $(`#${totalPriceId}`).text(totalPriceToShow);

    $.cookie(CookieName, JSON.stringify(items), { expires: 2, path: "/" });

    UpdateCart();
}