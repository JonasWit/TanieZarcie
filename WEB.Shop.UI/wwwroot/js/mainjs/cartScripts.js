﻿var addOneToCart = function (e) {
    var stockId = e.target.dataset.stockId;
    axios.post("/Cart/AddOne/" + stockId)
        .then(res => {
            updateCart();
        })
        .catch(err => {
            alert(err.error);
        })
}

var removeOneFromCart = function (e) {
    var stockId = e.target.dataset.stockId;
    removeFromCart(stockId, 1);
}

var removeAllFromCart = function (e) {
    var stockId = e.target.dataset.stockId;
    var el = document.getElementById("stock-quantity-" + stockId);
    var quantity = parseInt(el.innerText);
    removeFromCart(stockId, quantity);
}

var removeFromCart = function (stockId, quantity) {
    axios.post("/Cart/Remove/" + stockId + "/" + quantity)
        .then(res => {
            updateCart();
        })
        .catch(err => {
            alert(err.error);
        })
}

var updateCart = function myfunction() {
    axios.get('/Cart/GetCartComponent')
        .then(res => {
            var html = res.data;
            var el = document.getElementById("cart-nav");
            el.outerHTML = html;
        })
    axios.get('/Cart/GetCartMain')
        .then(res => {
            var html = res.data;
            var el = document.getElementById("cart-main");
            el.outerHTML = html;
        })
}