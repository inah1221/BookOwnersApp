import { useEffect, useState } from 'react';
import type BooksByAgeCategory from '../interfaces/booksByAgeCategory';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';

export default function BookOwners() {
  const API_URL = import.meta.env.VITE_API_URL;
  const [bookOwnersList, setBookOwnersList] = useState<BooksByAgeCategory[]>(
    []
  );

  useEffect(() => {
    fetch(`${API_URL}/api/BookOwner/GetBooks`)
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP ${response.status}: ${response.statusText}`);
        }
        return response.json();
      })
      .then((data) => {
        setBookOwnersList(data);
      })
      .catch((error) => console.error('Fetch failed:', error));
  }, []);

  return (
    <>
      {bookOwnersList.map((bookOwner) => {
        return (
          <Accordion defaultExpanded={true}>
            <AccordionSummary key={bookOwner.ownerAgeCategory}>
              {bookOwner.ownerAgeCategory}
            </AccordionSummary>
            {bookOwner.booksByAge.map((book) => (
              <AccordionDetails key={book.name}>
                {book.name} - {book.type}
              </AccordionDetails>
            ))}
          </Accordion>
        );
      })}
    </>
  );
}
