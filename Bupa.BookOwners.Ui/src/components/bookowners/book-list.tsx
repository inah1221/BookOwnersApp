import AccordionDetails from '@mui/material/AccordionDetails';
import { useMemo } from 'react';
import BookItem from './book-item';
import EmptyList from '../shared/empty-list';
import { ALL } from '../../constants';
import type Book from '../../types/book';

interface BookListProps {
  books: Book[];
  selectedBookType: string;
}

export default function BookList({ books, selectedBookType }: BookListProps) {
  const filteredBooks = useMemo(() => {
    const bookArray = books ?? [];
    if (!selectedBookType || selectedBookType === ALL) {
      return bookArray;
    }
    return bookArray.filter((book: Book) => book.type === selectedBookType);
  }, [books, selectedBookType]);

  return (
    <>
      <AccordionDetails sx={{ textAlign: 'left' }}>
        {filteredBooks.length === 0 && <EmptyList />}
        {filteredBooks.map((book: Book) => (
          <BookItem key={book.name} name={book.name} type={book.type} />
        ))}
      </AccordionDetails>
    </>
  );
}
