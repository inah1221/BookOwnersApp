import type Book from '../../types/book';

export default function BookItem({ name, type }: Book) {
  return (
    <>
      <p>
        {name} ({type})
      </p>
    </>
  );
}
