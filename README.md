# Book Owners App

## Setup

- Front-end uses vite for build/run.
- Run npm install.
- Use "npm run dev" command to run the UI.
- Should there be any changes in the port number of the API, it can be reconfigured in the .env file.

## Assumptions

- The book list must be distinct per category.
- Adult and children books are mutually exclusive.

## Authentication

- Guest tokens are created when app is first loaded for authentication.
- Guest tokens are only valid for an hour. They must be deleted from local storage before refreshing the app again.
