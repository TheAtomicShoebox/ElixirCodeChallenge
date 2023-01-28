# ElixirCodeChallenge
 
<p>
I used a basic SQL setup. Run the Setup.sql script to setup the database.<br/>
The connection string will likely be different for you than what it was for me.<br/>
Once you initially setup the database, get the connection string, and put it into the DataAccessBase class into the ConnectionString const.<br/>
This will have to be done for each version of the code.<br/>
Instead of writing several stored procedures, I went for direct SQL text. This is mainly to limit the size of the setup sql file, even though normally I would stay away from SQL text.<br/>
I made a version in .NET 4.7.2, and a version in .NET 6. Ultimately, there is very little difference between the two in terms of actual code.<br/>
I used the <code>Main()</code> method as the flow control method, and had it call other methods for each other step of the application.<br/>
</p>