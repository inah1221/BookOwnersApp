# Book Owners App

## Setup

- Front-end uses vite for build/run.
- Run npm install.
- Use "npm run dev" command to run the UI.

## Assumptions

- The book list must be distinct per category.
- Adult and children books are mutually exclusive.

## Authentication

- Guest tokens are created when app is first refreshed for authentication.
- Guest tokens are only valid for an hour. They must be deleted from local storage before refreshing the app again.
