import type Book from "./book";

export default interface BooksByAgeCategory {
    ownerAgeCategory: string;
    booksByAge: Book[];
}