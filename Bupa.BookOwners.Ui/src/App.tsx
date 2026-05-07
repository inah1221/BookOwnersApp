import { useEffect } from 'react';
import './App.css';
import BookOwners from './components/bookowners/book-owners';
import { API_URL } from './constants';

function App() {
  const token = localStorage.getItem('guestToken');
  const isAuthorized = token !== null;

  useEffect(() => {
    const initializeAuth = async () => {
      if (!localStorage.getItem('guestToken')) {
        const response = await fetch(`${API_URL}/api/Auth/GetGuestToken`);
        const data = await response.json();
        localStorage.setItem('guestToken', data.token);
      }
    };

    initializeAuth();
  }, []);

  return (
    <>
      {isAuthorized && <BookOwners />}{' '}
      {!isAuthorized && (
        <div>Refresh to get guest token for authorization.</div>
      )}
    </>
  );
}

export default App;
