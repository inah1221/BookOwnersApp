import { useEffect, useState } from 'react';
import type BooksByAgeCategory from '../../interfaces/booksByAgeCategory';
import {
  Button,
  Grid,
  MenuItem,
  Select,
  AccordionSummary,
  Accordion,
  Typography,
} from '@mui/material';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import BookList from './book-list';
import { ALL } from '../../constants';
import {
  fetchBookOwners,
  fetchBookTypes,
} from '../../services/bookOwnerService';
import './book-owners.scss';
import EmptyList from '../shared/empty-list';

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
    fetchBookOwners()
      .then((data) => setBookOwnersList(data))
      .catch((error) => {
        console.error(error);
        setBookOwnersList([]);
      });
  };

  const handleSelect = (event: any) => {
    setSelectedBookType(event.target.value as string);
  };

  return (
    <div className="book-owners-container">
      <Typography variant="h4" component="h1" gutterBottom>
        Book List By Age
      </Typography>
      <Grid container spacing={3} className="filter-container">
        <Grid size={2}>Filter by Book Type:</Grid>
        <Grid size={5}>
          <Select
            fullWidth
            variant="standard"
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
        </Grid>
        <Grid size={3}>
          <Button variant="outlined" onClick={() => handleClick()}>
            Refresh Books
          </Button>
        </Grid>
      </Grid>
      {bookOwnersList.length === 0 && <EmptyList />}
      {bookOwnersList.map((bookOwner) => {
        return (
          <Accordion defaultExpanded={true} key={bookOwner.ownerAgeCategory}>
            <AccordionSummary
              expandIcon={<ArrowDropDownIcon />}
              key={bookOwner.ownerAgeCategory}
            >
              <Typography variant="h6" component="span">
                {bookOwner.ownerAgeCategory}
              </Typography>
            </AccordionSummary>
            <BookList
              books={bookOwner.booksByAge}
              selectedBookType={selectedBookType}
            />
          </Accordion>
        );
      })}
    </div>
  );
}
