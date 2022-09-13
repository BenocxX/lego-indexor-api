SET search_path TO public;

CREATE TABLE accounts (
    user_id serial PRIMARY KEY
);

INSERT INTO accounts(user_id) VALUES (1);
INSERT INTO accounts(user_id) VALUES (2);
INSERT INTO accounts(user_id) VALUES (10);
