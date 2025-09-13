import { getCartData } from "./model.js";

export const renderProductsView = list => {
    list.forEach(cat => {
        document.querySelector(".products").insertAdjacentHTML("beforeend", `
            <div class="product__card">
                <img class="product__image" src="${cat.image}" alt="category">
                <h2 class="product__title">${cat.title.slice(0, 20) + '...'}</h2>
                <h4 class="product__price" >${cat.price}$</h4>
                <button class="product__cart--button" data-id=${cat.id}>Add to Cart</button>
            </div>
        `)
    })
    document.querySelectorAll(".product__cart--button").forEach((btn, index) => {
        btn.addEventListener("click", () => {
            let itemId = +btn.getAttribute("data-id");

            let cartData = getCartData();
            let item = list.find(item => item.id === itemId);
            let existingItem = cartData.find(cartItem => cartItem.id === itemId);
            if (!existingItem) { 
                cartData.push({ ...item, quantity: 1 });
            } else { 
                existingItem.quantity += 1;
            }
            
            localStorage.setItem("cartData", JSON.stringify(cartData));

            window.location.href = "./html/cart.html"
        });
    });
};

export const renderCartProductsView = list => {
    
    document.querySelector(".cart__items").innerHTML = ``;

    if(!list.length) {
        document.querySelector(".cart__items").insertAdjacentHTML("beforeend", `
            <div class="empty__cart--container">
                <h1 class="empty__cart">Empty Cart <span class="home__button">🏠</span></h1>
            </div>
        `);
        document.querySelector(".home__button").addEventListener("click", () => {
            window.location.href = "../index.html";
        })
    }
    else {
        list.forEach(item => {
    
            document.querySelector(".cart__items").insertAdjacentHTML("beforeend", `
                <div class="cart__item">
                    <div class="cart__image--container">
                        <img class="cart__image" src="${item.image}"/>
                    </div>
                    <div class="item__title--container">
                        <h4 class="item__title">${item.title}</h4>
                        <p class="item__description">${item.description}</p>
                    </div>
                    <table>
                        <tr>
                            <td class="price__header">Price</td>
                            <td class="price__header">Quantity</td>
                            <td class="price__header">Total</td>
                            <td class="price__header">Remove</td>
                        </tr>
                        <tr>
                            <td>${item.price}$</td>
                            <td>
                                <span class="increase" data-cart-id=${item.id}>&#43;&nbsp;&nbsp;</span>
                                ${item.quantity || 1} 
                                <span class="decrease" data-cart-id=${item.id}>&nbsp;&nbsp;&#8722;</span>
                             </td>
                            <td>
                                ${(item.price * (item.quantity || 1)).toFixed(2)}
                            </td>
                            <td class="remove" data-cart-id=${item.id}>&#10060;</td>
                        </tr>
                    </table
                </div>
            `);
    
            // listeners 
            document.querySelectorAll(".increase").forEach((btn, index) => {
                btn.addEventListener("click", () => {
    
                    let itemId = +btn.getAttribute("data-cart-id");
                    let item = list.find(item => item.id === itemId)
                    item.quantity = (item.quantity || 1) + 1
    
                    localStorage.setItem("cartData", JSON.stringify(list));
                    renderCartProductsView(list)
                    renderCheckoutView(list);
                });
            });
            document.querySelectorAll(".decrease").forEach((btn, index) => {
                btn.addEventListener("click", () => {
    
                    let itemId = +btn.getAttribute("data-cart-id");
                    let item = list.find(item => item.id === itemId)
                    console.log(item?.quantity);
                    if(item.quantity > 1) item.quantity--;
    
                    localStorage.setItem("cartData", JSON.stringify(list));
                    renderCartProductsView(list)
                    renderCheckoutView(list);
                });
            });
            document.querySelectorAll(".remove").forEach((btn, index) => {
                btn.addEventListener("click", () => {
    
                    let itemId = +btn.getAttribute("data-cart-id");
                    list = list.filter(item => item.id != itemId)
    
                    localStorage.setItem("cartData", JSON.stringify(list));
                    renderCartProductsView(list);
                    renderCheckoutView(list);
                });
            });
        })
        renderCheckoutView(list);
    }

}

const renderCheckoutView = list => {
    document.querySelector(".checkout").innerHTML = ``;
    console.log(list.reduce((acc, cur) => acc + (cur.quantity * cur.price), 0));
    if(list.length !== 0 ) {
        document.querySelector(".checkout").insertAdjacentHTML("beforeend", `
            <div>
                <h1 class="total__price--all">Total Price: ${list.reduce((acc, cur) => acc + (cur.quantity * cur.price), 0).toFixed(2)}</h1>
                <button class="checkout__button">Checkout</button>
                <button class="checkout__button checkout__button--home">Back 🏠</button>
            </div>    
        `)
        document.querySelector(".checkout__button").addEventListener("click", () => {
            alert("Cart Purchased")
            window.location.href = "../index.html";
            localStorage.setItem("cartData", JSON.stringify([]))
        })
        document.querySelector(".checkout__button--home").addEventListener("click", () => {
            window.location.href = "../index.html";
        })
        
    }
}