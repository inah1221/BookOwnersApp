import AccordionDetails from '@mui/material/AccordionDetails';
import type Book from '../../interfaces/book';

export default function BookItem({ name, type }: Book) {
  return (
    <>
      <AccordionDetails key={name}>
        {name} - {type}
      </AccordionDetails>
    </>
  );
}
