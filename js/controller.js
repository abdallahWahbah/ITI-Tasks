import { getProducts } from "./model.js";
import { renderProductsView } from "./view.js";

const renderProducts = async () => {
    try {
        let res = await getProducts();
        console.log(res);
        renderProductsView(res);
    }
    catch(error) {
        console.log(error);
    }
    
}
renderProducts();