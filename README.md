# CowboyApi

This is a simple web api  project written in C#.

## Project strcuture

1. Entities

   This folder contains the cowboy entity.
2. Repositories

   This folder contains classes and methods for doing basic CRUD operations on the database. 
3. Services

   This folder contains classes and methods which allows the user to perform CRUD operation on the cowboy entity as well as shoot and reload the gun. All the business logic is implemented at this layer.
4. Controllers

   This folder contains classes and methods for the REST api  which can be used by the user to call this service.

## List of REST api's

| Method | URI | Description |
|---|---|---|
| GET | /api/v1/cowboy/list | Gets a list of all cowboys |
| GET | /api/v1/cowboy/{id} | Get a single cowboy by Id |
| POST | /api/v1/cowboy/ | Create a new cowboy |
| DELETE | /api/v1/cowboy/{id} | Delete a cowboy by Id |
| PATCH  |  /api/v1/cowboy?id={id} | Update a existing cowboy  |
| POST | /api/v1/cowboy/shoot/{id} | Ask a cowboy to shoot his gun |
| POST | /api/v1/cowboy/reload/{id} | Ask a cowboy to reload his gun |
| POST | /api/v1/cowboy/shootout?FirstCowboyId={id}&SecondCowboyId={id} | Simulate a shootout between two cowboys |

## [Postman document](/Cowboy%20API.postman_collection.json)

## Game Algorithm

The code simulates a shootout between two cowboys, with each round randomly determining whether the shooter hits the opponent based on their accuracy. The shootout continues until one cowboy's health reaches zero or after five rounds.

* check if first cowboy id is valid
* check if second cowboy id is valid
* randonly select which cowboy shoots first
* initlize loop counter to zero.
* while loop counter is less than five and no player health is zero
    * if current player has enough bullets, then fire gun
        * if current player hits target
            * deduct health points from opponent and reduce number of bullets in gun.
        * if current player misses target
            * Reduce number of bullets from gun
     * else Reload gun
     * swap current player with opponent.
     * increment loop counter.

## known limitation

The project is ment to showcase basic understanding of web api and coding skills and should not be used in production. It lacks . . . 

1. CI & CD pipeline with with unit/integration tests coverage.
2. Error and information Logging.
3. Input validation.
4. Authentication or authorization
5. An API gateway for caching and rate limiting policies.
6. No code comments

## API security

In order to secure the cowboy API, we can implement client credentails flow from OAUTH 2.0. This involves an application exchanging its credentials, such as client ID and client secret, for an access token.

This flow is best suited for Machine-to-Machine (M2M) applications, such as CLIs, daemons, or backend services.

![client credentails flow](/client-credentails-flow.png)

* Client sends client id and secret with scope to identity provider (idp)
* idp provides a token to the client
* Client will add the token to the HTTP header, when calling the API
* the API will validate the token using the idp.

Token format is JWT (JSON web token)

HTTPS is a must when using jwt tokens

## Production ready

1. Support sorting and paging
2. Validate user data
3. Versioning API
4. Implement rate limiting
5. Implement logging
6. Implement monitoring
7. API Documentation for devs