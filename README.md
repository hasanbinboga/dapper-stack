# dapper-stack
I am posting the project architecture that I have developed in the projects I have worked on before in this repository. The project architecture includes applying the Unitofwork pattern to the Dapper ORM.

Firstly I create Common Exception library. This library contains exception classes for the system. 
After that I create library for common helpers and extensions. It includes convert, crypto, data, json and string extensions.
Then I created the Core library. This library contains base entity classes. In addition, the classes of data transfer objects to be used by the services are also in this library.
Then, I designed the library that includes the basic data access classes with the UnitOfWork pattern applied on the Dapper ORM, especially the data access, type conversion and repository classes.

I decided to use public database for tests and development. Database connection string is shared on project config file. You can connect to the database. Sample database is had own entiies.
![image](https://user-images.githubusercontent.com/27738643/209881065-640de29f-781a-4c2f-a9d0-f7ca5af6cd60.png)
