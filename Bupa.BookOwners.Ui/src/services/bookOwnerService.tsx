import { API_URL } from '../constants';
import type BooksByAgeCategory from '../types/booksByAgeCategory';

export const fetchBookOwners = async (): Promise<BooksByAgeCategory[]> => {
  const headers = setHeaders();
  const response = await fetch(`${API_URL}/api/BookOwner/GetBooks`, {
    headers,
  });
  if (!response.ok) {
    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
  }
  return await response.json();
};

export const fetchBookTypes = async (): Promise<string[]> => {
  const headers = setHeaders();
  const response = await fetch(`${API_URL}/api/BookOwner/GetBookTypes`, {
    headers,
  });
  if (!response.ok) {
    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
  }
  return await response.json();
};

export const setHeaders = () => {
  const token = localStorage.getItem('guestToken');
  return {
    'Content-Type': 'application/json',
    Authorization: `Bearer ${token}`,
  };
};
