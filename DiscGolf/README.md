## Docker

~~~bash
docker build -t webapp:latest .

docker buildx build --progress=plain --force-rm --push -t joklem/webapp:latest .
~~~