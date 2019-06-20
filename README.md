[![Gitter](https://badges.gitter.im/Facebook-ASP-NET-Webhook/fb-asp-net-webhook.svg)](https://gitter.im/Facebook-ASP-NET-Webhook/fb-asp-net-webhook?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

# Retrieving facebook leads using ASP.NET Web API and Graph API Webhooks

ASP.NET way of retrieving Facebook Lead Ad data

## Prerequisites

* Facebook developer account
* Visual Studio 2015 or higher

## Web API Implementation

* Compile and host the Web API in your server<br/>
<b>Note:</b> SSL Certificate is mandatory. Facebook accept only https URL.

* When an lead ad is generated facebook will post the data to the Web API's <b>Post</b> action.<br/>
![api](https://user-images.githubusercontent.com/17797942/29604521-63147678-8805-11e7-96c7-93ff359cbb77.png)
* Following data is avaliable in the API<br/>
![post_data](https://user-images.githubusercontent.com/17797942/29604579-9cc08740-8805-11e7-8357-b03d1577bcb5.png)


## Facebook App & Webhook Configuration

* Navigate to <https://developers.facebook.com>
* Choose <b>Add a New App</b> from the <b>My Apps</b> pull-down menu<br/>
![Add facebook app](https://user-images.githubusercontent.com/17797942/29600729-3a0e261c-87f5-11e7-87d2-07be3a50d8cb.png)

* Provide the display name, contact email address and click <b>Create App ID</b><br/>
![New app](https://user-images.githubusercontent.com/17797942/29601181-c06e31d2-87f7-11e7-89c1-6936160f2840.png)

* Navigate to <b>Add Product</b> and setup Webhook <br/>
![add product](https://user-images.githubusercontent.com/17797942/29601301-7625b6e4-87f8-11e7-9b1c-38bf8dbdb5c8.png)<br/>
![webhook](https://user-images.githubusercontent.com/17797942/29603525-20d6e406-8802-11e7-88b6-f607db1c1643.png)

* Select <b>Page</b> from the pull-down menu and click <b>Subscribe to this topic</b><br/>
![subscription](https://user-images.githubusercontent.com/17797942/29603667-a4907f14-8802-11e7-8483-37942b7d7d4d.png)

* Provide the callback URL <b>(Note: SSL is mandatory)</b>, <b>Verification Token</b> and click <b>Verify and Save</b><br/>
![subscription_config](https://user-images.githubusercontent.com/17797942/29603838-3bbe6b6c-8803-11e7-91b2-fac4527b5e4b.png)

* On successfull verification you will be prompted with a list of avaliable subscriptions. From the list subscribe to <b>Leadgen</b><br/>
![ledgen_subscribtion](https://user-images.githubusercontent.com/17797942/29604062-e767687e-8803-11e7-8c23-6bec68ac3ea8.png)<br/>
![subscription_confirmation](https://user-images.githubusercontent.com/17797942/29604094-080f1ad6-8804-11e7-821b-ef4b05b5e5e1.png)

* To test the working without creating a lead ad do the following
    * Cick <b>Test</b> button in the <b>leadgen</b> subscription<br/>
    ![test_leadgen](https://user-images.githubusercontent.com/17797942/29604905-9ba23600-8806-11e7-8696-1c5070d78baa.png)
    * Facebook will open-up a popup with sample data, click <b>Send to My Server</b> button.<br/>
    ![sample_data](https://user-images.githubusercontent.com/17797942/29604961-d39b439e-8806-11e7-9eed-c46082227a2a.png)
    * If there is any valid method like sending email, writing to database etc is implemented in the API post will get executed.

## License

This project is licensed under the MIT License
