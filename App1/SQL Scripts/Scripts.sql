CREATE TABLE users (
	id SERIAL PRIMARY KEY,
	username TEXT NOT NULL,
	password TEXT NOT NULL
);

create table cities
(
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

CREATE TABLE orders 
(
    order_id SERIAL PRIMARY KEY,
    client_name VARCHAR(100) NOT NULL,
    cargo_type VARCHAR(100) NOT NULL,
    cargo_weight DECIMAL(10, 2) NOT NULL,
    source_city VARCHAR(100) NOT NULL,
    destination_city VARCHAR(100) NOT NULL
)
CREATE TABLE deliveries
(
    delivery_id SERIAL PRIMARY KEY,
    order_id INT NOT NULL,
    status VARCHAR(100) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
)


