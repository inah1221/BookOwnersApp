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
      {token !== null && <BookOwners />}
      {token === null && (
        <div>Guest token created. Refresh to see list of books.</div>
      )}
    </>
  );
}

export default App;
