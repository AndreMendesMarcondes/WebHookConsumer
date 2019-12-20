# WebHookConsumer
WebHookConsumer
- Clone this repository
- Open project with visual studio
- Restore Project
- Configure these two enviroment variables
    - HookURL: ws://localhost:8080/quotes
    - PostURL: https://localhost:5001/quote (or the url you want hook post data)
- With docker installed run docker run -p 8080:8080 toroinvestimentos/quotesmock
- run app
