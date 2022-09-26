SET search_path TO public;

create table "user"
(
    id       serial
        constraint user_pk
            primary key,
    username varchar(255),
    password varchar(255)
);

alter table "user"
    owner to dev;

create unique index user_id_uindex
    on "user" (id);

INSERT INTO public."user" (id, username, password) VALUES (1, 'Mathis', 'Omega123*');
