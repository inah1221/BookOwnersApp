import { useEffect, useState } from 'react';
import type BooksByAgeCategory from '../interfaces/booksByAgeCategory';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import { Button, MenuItem, Select } from '@mui/material';
import BookList from './book-list';
import { ALL, API_URL } from '../constants';

export default function BookOwners() {
  const [bookOwnersList, setBookOwnersList] = useState<BooksByAgeCategory[]>(
    []
  );

  const [bookTypes, setBookTypes] = useState<string[]>([]);
  const [selectedBookType, setSelectedBookType] = useState<string>(ALL);

  const fetchBookOwners = async () => {
    const response = await fetch(`${API_URL}/api/BookOwner/GetBooks`);
    if (!response.ok) {
      throw new Error(`HTTP ${response.status}: ${response.statusText}`);
    }
    const jsonData = await response.json();
    setBookOwnersList(jsonData);
  };

  const fetchBookTypes = async () => {
    const response = await fetch(`${API_URL}/api/BookOwner/GetBookTypes`);
    if (!response.ok) {
      throw new Error(`HTTP ${response.status}: ${response.statusText}`);
    }
    const jsonData = await response.json();
    setBookTypes(jsonData);
  };

  useEffect(() => {
    fetchBookOwners();
    fetchBookTypes();
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
