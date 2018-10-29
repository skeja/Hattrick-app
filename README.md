# Hattrick demo app

This is simple online betting application

## TO DO:
- [ ] Cleanup Vuex store
- [ ] Make Vuex mutation atomic and sync
- [ ] Revork and reduce number of Vuex actions
- [ ] Cleanup API, make more sensible responses data

## Dependencies

- Node.js
- Npm
- PostgreSQL
- ASP.NET core

## Instalation

1. Clone this repo
2. Run `dotnet restore` to restore .NET packages
3. Run `npm install` to install npm packages
4. To seed database run `dotnet ef migrations add InitialMigration`

## Launch

- For development:
    - `npm run dev`
- For production
    - `npm run build`
    - `npm start`

## Resources

- [ASP.NET + VUE.JS template](https://github.com/MarkPieszak/aspnetcore-Vue-starter)
