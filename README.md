# PostCardAppV2
Second edition of the asp.net Postcard app

This web app calculates quotes for postcards using postcard size, print color, paper type, and a few other postcard details. 

the application automatically searches a list of uploaded paper stocks and selects the smallest possible sheet which fits the largest number of postcards. The price per sheet for that paper is then used in calculation. 

Printing types (double sided, doubled sided with color, etc...) have an assigned cost on the website which is editable by the user, this price is also used inside the quote calculation. 

All these previous variables are combined with some static quantity cost lookup tables and the quote details in order to calculate cost for a given estimate in a quote. Users can calculate and save up to 20 estimates in a quote, and once saved, each quote remains in the online database. the database recycles quotes monthly, and reports them to the owner in the form of an emailed csv. 

The email system uses Mailgun as an SMTP service and schedules the monthly cleanup using a cron expression. 

Quotes are saved in a string format, but each estimate is mapped to a quote as a forign relation to save space on string characters. 

Paper costs can be uploaded directly from a csv file, and the program will automatically calculate compatible postcard sizes, and then save sheet costs. 


