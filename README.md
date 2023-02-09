# Pokemon API
RESTful API that fetches information about a specific Pokemon and returns its description in Shakespearean language.

## Concepts

The Pokemon api input use the following parameters:
 - `IdOrName` - You can use Id or name of pokemon
 - `language` - You can define specific language to translate
#
The Pokemon api output contains:
 - `FlavorText` - The id of the entity associated with this result
 - `Language`- language
 - `Version` - version
 - `ShakespeareanDescription` - The translation of flavor text

 ## Containerize
 The API is Containerize using Docker
 to build the image for the first time ```docker build .  -t pokemon:0.1```
 to run the image ```docker run -p 5000:80 pokemon:01``` and use postman to call  http://localhost:5000/api/pokemon/1 locally

## TODO list
- Error handling : handle exceptions and return appropriate error messages to the client in case of failure.
- Caching : cache the translated descriptions to avoid calling the Translations API for the same Pokemon multiple times.
 This will reduce the number of API calls and improve the performance of the API.
- Test coverage : write more tests to cover more scenarios and to ensure that the API is robust and scalable.write additional tests to cover various scenarios, 
such as testing with invalid Pokemon names or IDs, and testing error handling
- Logging : to track the API's usage and to debug any issues
- Documentation : provide comprehensive documentation in code
- Authentication and authorization : If the API needs to be secure, we should implement authentication and authorization mechanisms, such as OAuth or JWT
- Adding try policy with polly for httpclient
- Maybe using hangfire to handle ratelimit


