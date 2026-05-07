import AccordionDetails from '@mui/material/AccordionDetails';
import { useMemo } from 'react';
import BookItem from './book-item';
import { ALL } from '../constants';

export default function BookList({ books, selectedBookType }: any) {
  const filteredBooks = useMemo(() => {
    const bookArray = books ?? [];
    if (!selectedBookType || selectedBookType === ALL) {
      return bookArray;
    }
    return bookArray.filter((book: any) => book.type === selectedBookType);
  }, [books, selectedBookType]);

  return (
    <>
      {filteredBooks.length === 0 && <p>No books found.</p>}
      {filteredBooks.map((book: any) => (
        <AccordionDetails key={book.name}>
          <BookItem name={book.name} type={book.type} />
        </AccordionDetails>
      ))}
    </>
  );
}
