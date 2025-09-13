export const getProducts = async () => {
    try {
        const res = await fetch("https://fakestoreapi.com/products");
        const data = await res.json();
        return data;
    } catch (error) {
        console.log(error);
    }
};

export const getCartData = () =>{
    return JSON.parse(localStorage.getItem("cartData")) || [];
}