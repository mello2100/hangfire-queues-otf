# hangfire-queues-otf
A simple hangfire wrapper that lets you manipulate workers on the fly

Hangfire allows you to pass a list of IBackgroundProcess to a BackgroundProcessingServer. That way we can individually change the hangfire workers configuration (queue name and quantity).
The idea of this simple project came when we had the need to dynamically change the number of workers allocated to each queue.

In this lib we execute a server on a task that receives a cancellation token and reloads the server on a new task. The new task will be executing without any delay (executes immediately) while the older task waits for the executing workers to terminate (given a maximum timeout).

The main project is given by Hangfire.Queues.OTF assembly.
Two more projects were added to test our proof of concept (POC), a console application and another assembly which holds the job methods.

We haven't provided any automated tests by know, but it will be reconsidered as soon as we feel the need to increase functionalities.
