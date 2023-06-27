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

7. 