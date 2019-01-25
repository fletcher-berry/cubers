# cubers
This app stores rubik's cube solves for multiple cubers for 3x3, 3x3 one handed, and 4x4 events.  The app allows for the creation, update and deletion of data.  The front end is written in Angular and the back end is an API written in C#. 

## To run
- Set up back end
  - Install MySql.data.core from Nuget
  - Set up a MySql database to store data.  Schema for DB is in schema.sql.
  - (optional) Add sample data.  Run inserts.sql.
  - In appsettings.json, set the database connection string to connect to your database. (change the password)
- set up front end
  - cd to front end directory
  - npm install
  - in src/environments/environment.ts set the server address to the address of the running back end
- run with 'ng serve'
