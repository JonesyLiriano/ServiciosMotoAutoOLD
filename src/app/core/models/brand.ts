import { Product } from "./product";

export class Brand {
    name: string | undefined;
    slogan: string | undefined;
    description: string | undefined;
    category: string | undefined;
    logo: string | undefined;
    products: Product[] | undefined;
}
