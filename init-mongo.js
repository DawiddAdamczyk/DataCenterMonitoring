print("RunConfig!")

db.createUser({
    user: 'dev1',
    pwd: 'password1',
    roles: [
      {
        role: 'readWrite',
        db: 'server-db',
      },
    ],
  });



