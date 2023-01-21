import { BookModel } from "./BookModel";

export class CartModel {
 id?: number;
 userId!: number;
 bookId!: number;
 quantity!: number;
}

export class CartList {
 id!: number;
 userId!: number;
//  book!: BookModel;
bookId!:number;
bookName!: string;
price!:string;
 quantity!: number;
 base64image! :string;
}

export class GetCart {
 results!: CartList[];
 totalRecords!: number;
}
