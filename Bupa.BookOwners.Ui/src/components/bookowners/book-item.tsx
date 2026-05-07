import type Book from '../../interfaces/book';

export default function BookItem({ name, type }: Book) {
  return (
    <>
      <p>
        {name} - {type}
      </p>
    </>
  );
}
