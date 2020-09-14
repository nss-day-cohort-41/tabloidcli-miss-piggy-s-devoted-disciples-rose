# Tabloid CLI - Miss Piggy's Divoted Deciples Blogger'

## The Tabloid Proof of Concept

Our new business will create a software product to allow people save info about favorite blog posts, authors and blogs.
In order to test the new business idea, we'll create a [Proof of Concept (POC)](https://en.wikipedia.org/wiki/Proof_of_concept#Software_development). This POC will be a simplified implementation of the idea that will be used to test the business idea to ensure that customers would find it valuable, and also to allow the team to get a handle on the concept. After we complete the POC we will use it to evaluate our business idea and determine if we need to pivot (change direction) toward another idea.
For our POC, we will build a command line app in C# and save our data in SQL Server.

## Getting Started

### Setup
1. Clone and enter the project directory
2. Open Visual Studio 
3. From the start menu open the project Solution File
4. From the File menu -> open file -> TabliodCLI.SQL
5. Run the script to create a database on your local server / machine.
6. From Debugger menu open Start Debugger or hit "F5" to access the application.

### User Experience
1. Once Command Line Application is started you are presented with a list of options
	-My Journal Management
	-Blog Management
	-Author Management
	-Post Management
	-Tag Management
	-Search by Tag
	-Change Color Scheme
	-Exit
2. Choose a number that respresents the area you would like to access.
3. Within each Journal, Bloog, Author, Post, Tag area you will have access to Add, Delete, Edit, and view details.
4.Within Color Scheme you are able to change the color scheme for your console application.

#### Example of accessing Journal Managment
> ADD JOURNAL ENTRY
from journal menu type "2" and enter to add a journal entry
type journal title, hit enter
type jounrnal entry, hit enter

> EDIT JOURNAL ENTRY
from journal menu type "3" and enter to edit a journal entry
type the number representation for your journal entry and hit enter
type new journal title, hit enter (or hit enter to skip)
type new journal content, hit enter (or just hit enter to skip)
your entry is now updated
from journal menu type "1" and enter to view list of journal entries and including the updated entry

> DELETE JOURNAL ENTRY
from journal menu type "4" and enter to delete a journal entry
type the number representation for your journal entry and hit enter
your entry is now deleted
from journal menu type "1" and enter to view list of journal entries without the entry that was just deleted
Exit type "0" hit "enter"

### Technologies Used:
Visual Studio<br>
CSharp<br>
SQL Server<br>
ADO.NET<br>

### Authors:
> Tasha Lane<br>
> Chad Jones<br>
> Brett Stoudt<br>
> Gavin Swofford<br>
