# OmdbWebApplication

Write a simple ASP.NET Core web application that queries the OMDb API to retrieve information about movies/shows.
It should consist of:
1)	Search page (search box where user enters the movie he is searching for, e.g. “terminator”)
2)	Search results page, where the movies/shows that match the search phrase are presented. You can present the search results as a small table (don’t focus on pretty UI etc, use very basic bootstrap table structures). Clicking on each movie result should take you to point 3 below, which is:
3)	Movie page. The movie page will present all the information gathered for a particular movie. (e.g. name, year, synopsis, release date, runtime, genre, IMDb Rating + IMDb votes including hyperlink to IMDb for that movie). Again, just use a table, no fancy UIs.
4)	You should store this movie information in a database. If the movie is already in the database, return the results for the movie. If the movie does not exist in the database, you need to fetch it from this API and store it in the DB.
5)	Do not implement all logic in the action of the controller. You can layer your app (separate to namespaces, or even projects within the solution) with distinct responsibilities. 
API link: http://www.omdbapi.com/
