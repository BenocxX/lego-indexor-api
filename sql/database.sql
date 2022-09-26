SET search_path TO public;

create table "user"
(
    id       serial
        constraint user_pk
            primary key,
    username varchar(255) not null,
    password varchar(255)
);

alter table "user"
    owner to dev;

create unique index user_id_uindex
    on "user" (id);

create unique index user_username_uindex
    on "user" (username);

