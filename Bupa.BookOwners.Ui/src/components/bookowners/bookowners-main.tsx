import { useEffect, useState } from 'react';
import type BooksByAgeCategory from '../../interfaces/booksByAgeCategory';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import { Button, MenuItem, Select } from '@mui/material';
import BookList from './book-list';
import { ALL } from '../../constants';
import {
  fetchBookOwners,
  fetchBookTypes,
} from '../../services/bookOwnerService';

export default function BookOwners() {
  const [bookOwnersList, setBookOwnersList] = useState<BooksByAgeCategory[]>(
    []
  );

  const [bookTypes, setBookTypes] = useState<string[]>([]);
  const [selectedBookType, setSelectedBookType] = useState<string>(ALL);

  useEffect(() => {
    fetchBookOwners()
      .then((data) => setBookOwnersList(data))
      .catch((error) => {
        console.error(error);
        setBookOwnersList([]);
      });
    fetchBookTypes()
      .then((data) => setBookTypes(data))
      .catch((error) => {
        console.error(error);
        setBookTypes([]);
      });
  }, []);

  const handleClick = () => {
    fetchBookOwners();
  };

  const handleSelect = (event: any) => {
    setSelectedBookType(event.target.value as string);
  };

  return (
    <>
      <Button onClick={() => handleClick()}>Refresh Books</Button>
      <Select
        value={selectedBookType}
        onChange={($event) => handleSelect($event)}
      >
        <MenuItem key={ALL} value={ALL}>
          {ALL}
        </MenuItem>
        {bookTypes.map((bookType) => (
          <MenuItem key={bookType} value={bookType}>
            {bookType}
          </MenuItem>
        ))}
      </Select>
      {bookOwnersList.length === 0 && <p>No book owners found.</p>}
      {bookOwnersList.map((bookOwner) => {
        return (
          <Accordion defaultExpanded={true} key={bookOwner.ownerAgeCategory}>
            <AccordionSummary key={bookOwner.ownerAgeCategory}>
              {bookOwner.ownerAgeCategory}
            </AccordionSummary>
            <BookList
              books={bookOwner.booksByAge}
              selectedBookType={selectedBookType}
            />
          </Accordion>
        );
      })}
    </>
  );
}
