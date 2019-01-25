# cubers
This app stores rubik's cube solves for multiple cubers for 3x3, 3x3 one handed, and 4x4 events.  The app allows for the creation, update and deletion of data.  The front end is written in Angular and the back end is an API written in C#. 

## To run
- Set up back end
  - Install MySql.data.core from Nuget
  - Set up a MySql database to store data.  Schema for DB is in schema.sql.
  - (optional) Add sample data.  Run inserts.sql.
- set up front end
  - cd to front end directory
  - npm install
- run with 'ng serve'
