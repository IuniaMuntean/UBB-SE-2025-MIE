CREATE TABLE users (
id SERIAL PRIMARY KEY,
username TEXT NOT NULL,
password TEXT NOT NULL
);
create table cities(
	id int GENERATED ALWAYS AS identity primary key,
	name varchar(255),
	x float,
	y float	
);
create table roads
(
	startCity int,
	endCity int,
	value float,
	foreign key (startCity) references cities(id),
	foreign key (endCity) references cities(id),
	primary key (startCity, endCity)
);