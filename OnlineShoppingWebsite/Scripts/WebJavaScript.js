// Home/index
var slideIndex = 1;
showSlides();

function showSlides() {
    var i;
    var slides = document.getElementsByClassName("webSlides");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1; }
    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 5000);
}


function showProductDepartmentwise(department, div_id, name, id, image)
{
    var divHtml = "";

    if (department === "Arts & Crafts") {
        divHtml += '<div class="column text-block" onclick="ShowSingleProduct('+id+')">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src="/Image/ProductImages/'+image+'" />\
                </a>\<p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#arts_crafts").append(divHtml);
        divHtml = "";
    }

    else if (department === "Automotive") {
        divHtml += '<div class="column text-block" onclick="ShowSingleProduct(' + id +')">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#automotive").append(divHtml);
        divHtml = "";
    }

    else if (department === "Baby") {
        divHtml += '<div class="column text-block" onclick="ShowSingleProduct(' + id +')">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#" + div_id).append(divHtml);
        divHtml = "";
    }

    else if (department === "Beauty & Personal Care") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4><a href="/WebArea/Home/Product/' + id +'" >\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p></a>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#beauty_personal_care").append(divHtml);
        divHtml = "";
    }

    else if (department === "Books") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart(id)">Add To Cart</a></div>';

        $("#books").append(divHtml);
        divHtml = "";
    }

    else if (department === "Computer") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+id+')">Add To Cart</a></div>';

        $("#computer").append(divHtml);
        divHtml = "";
    }

    else if (department === "Digital Music") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#digitalmusic").append(divHtml);
        divHtml = "";
    }

    else if (department === "Electronics") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#electronics").append(divHtml);
        divHtml = "";
    }

    else if (department === "Kindle Store") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#kindlestore").append(divHtml);
        divHtml = "";
    }

    else if (department === "Prime Video") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#primevideo").append(divHtml);
        divHtml = "";
    }

    else if (department === "Women's Fashion") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#womensfashion").append(divHtml);
        divHtml = "";
    }

    else if (department === "Men's Fashion") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#mensfashion").append(divHtml);
        divHtml = "";
    }

    else if (department === "Girl's Fashion") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#girlsfashion").append(divHtml);
        divHtml = "";
    }

    else if (department === "Boy's Fashion") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#boysfashion").append(divHtml);
        divHtml = "";
    }

    else if (department === "Deals") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#deals").append(divHtml);
        divHtml = "";
    }

    else if (department === "Health & Household") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#health_household").append(divHtml);
        divHtml = "";
    }

    else if (department === "Home & Kitchen") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#home_kitchen").append(divHtml);
        divHtml = "";
    }

    else if (department === "Industrial & Scientific") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#industrial_scientific").append(divHtml);
        divHtml = "";
    }

    else if (department === "Luggage") {
        divHtml += '<div class="column text-block">\
                <h4>'+ name + '</h4>\
                <a href="/WebArea/Home/Product/' + id +'" >\
                <img src=" /Image/ProductImages/'+image+'" />\
                </a>\
                <p>'+ department + '</p>\
                <a onclick="Add_Product_To_Cart('+ id +')">Add To Cart</a></div>';

        $("#luggage").append(divHtml);
        divHtml = "";
    }
}

function Add_Product_To_Cart(ID)
{
    var cart = document.getElementById("cart-counting").innerText;

        var url = "/WebArea/Home/AddProductInCart";
        $.post(url, {ID: ID}, function (data) {
            document.getElementById("cart-counting").innerText = data;
        });
}


