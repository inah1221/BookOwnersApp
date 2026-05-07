import { useEffect, useState } from 'react';
import './book-owners.scss';
import type BooksByAgeCategory from '../../interfaces/booksByAgeCategory';
import {
  fetchBookOwners,
  fetchBookTypes,
} from '../../services/bookOwnerService';
import EmptyList from '../shared/empty-list';
import {
  Button,
  Grid,
  MenuItem,
  Select,
  AccordionSummary,
  Accordion,
  Typography,
  CircularProgress,
} from '@mui/material';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import BookList from './book-list';
import { ALL } from '../../constants';

export default function BookOwners() {
  const [bookOwnersList, setBookOwnersList] = useState<BooksByAgeCategory[]>(
    []
  );

  const [bookTypes, setBookTypes] = useState<string[]>([]);
  const [selectedBookType, setSelectedBookType] = useState<string>(ALL);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    setLoading(true);
    fetchBookOwners()
      .then((data) => setBookOwnersList(data))
      .catch((error) => {
        console.error(error);
        setBookOwnersList([]);
      })
      .finally(() => setLoading(false));
    fetchBookTypes()
      .then((data) => setBookTypes(data))
      .catch((error) => {
        console.error(error);
        setBookTypes([]);
      });
  }, []);

  const handleClick = () => {
    setLoading(true);
    fetchBookOwners()
      .then((data) => setBookOwnersList(data))
      .catch((error) => {
        console.error(error);
        setBookOwnersList([]);
      })
      .finally(() => setLoading(false));
  };

  const handleSelect = (event: any) => {
    setSelectedBookType(event.target.value as string);
  };

  return (
    <div className="book-owners-container">
      <Typography variant="h4" component="h1" gutterBottom align="left">
        Book List By Age Category
      </Typography>
      <Grid container spacing={3} className="filter-container">
        <Grid size={2}>Filter by Book Type:</Grid>
        <Grid size={4}>
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
      {loading && <CircularProgress aria-label="Loading…" />}
      {!loading && bookOwnersList.length === 0 && <EmptyList />}
      {!loading &&
        bookOwnersList.map((bookOwner) => {
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
