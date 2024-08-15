const productsList = $("Merge8").first().json.data;
const shopifyProducts = $("Merge8").last().json.products;

const productsUpdate = [];

for (const key in shopifyProducts) {
    if (shopifyProducts.hasOwnProperty(key)) {
        const product = shopifyProducts[key];

        for (const variant of product.variants) {
            for (const key2 in productsList) {
                if (productsList.hasOwnProperty(key2)) {
                    const prdtTotvs = productsList[key2];
                    let productFound = false;
                    for (const variantTotvs of prdtTotvs.variants) {
                        if (variantTotvs.sku == variant.sku) {
                            if (variant.price != variantTotvs.price || variant.inventory_quantity != variantTotvs.inventory_quantity) {
                                
                                
                                for (const existingProduct of productsUpdate) {
                                    if (existingProduct.product.id === variant.product_id) {
                                        existingProduct.product.variants.push({
                                            id: variant.id,
                                            price: variantTotvs.price,
                                            inventory_quantity: variantTotvs.inventory_quantity
                                        });
                                        productFound = true;
                                        break;
                                    }
                                }

                                if (!productFound) {
                                    console.log(variantTotvs)
                                    productsUpdate.push({
                                        product: {
                                            id: variant.product_id,
                                            variants: [
                                                {
                                                    id: variant.id,
                                                    price: variantTotvs.price,
                                                    inventory_quantity: variantTotvs.inventory_quantity
                                                }
                                            ]
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

return productsUpdate;