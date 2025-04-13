CREATE TABLE users (
	id SERIAL PRIMARY KEY,
	username TEXT NOT NULL,
	password TEXT NOT NULL
);
create table cities
(
	id int primary key,
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
);
CREATE TABLE delivery
(
    delivery_id SERIAL PRIMARY KEY,
    manager VARCHAR(50) NOT NULL DEFAULT 'Default Manager',
    departure VARCHAR(255) NOT NULL,
    destination VARCHAR(255) NOT NULL,
    distance DECIMAL(10, 2) CHECK (distance >= 0),
    driver VARCHAR(50),
    departure_time TIMESTAMP,
    arrival_time TIMESTAMP,
    truck_id INT CHECK (truck_id > 0),
    cargo_weight DECIMAL(10, 2) CHECK (cargo_weight >= 0),
    order_id INT NOT NULL REFERENCES orders(order_id) ON DELETE CASCADE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);