# DiscordBotSample

Git command to ignore App.config modification: ```git update-index --assume-unchanged **/files/App.config```

The "files" folder contains the App.config that holds your credentials and all the files you want to read/write from your code and your machine.

Docker Command to run container and map local folder to container folder:

```docker run -d --restart=always -v {YOUR_CUSTOM_PATH}:/app/files/ {YOUR_IMAGE_NAME_OR_ID}```

