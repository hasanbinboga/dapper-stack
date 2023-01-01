CREATE TABLE IF NOT EXISTS public.newcustomers
(
		"id" bigserial NOT NULL,
    customer_number integer NOT NULL,
    customer_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    contact_last_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    contact_first_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    phone character varying(50) COLLATE pg_catalog."default" NOT NULL,
    address_line1 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    address_line2 character varying(50) COLLATE pg_catalog."default" default NULL::character varying,
    city character varying(50) COLLATE pg_catalog."default" NOT NULL,
    state character varying(50) COLLATE pg_catalog."default" default NULL::character varying,
    postal_code character varying(15) COLLATE pg_catalog."default" default NULL::character varying,
    country character varying(50) COLLATE pg_catalog."default" NOT NULL,
    sales_rep_employee_number integer,
    credit_limit numeric(10,2) default NULL::numeric,
		is_deleted boolean default false,
		create_time timestamp NOT NULL,
		update_time timestamp,
		create_ip_address inet NOT NULL,
		update_ip_address inet,
		create_user_name character varying(255) NOT NULL,
		update_user_name character varying(255),
		history jsonb,
	  PRIMARY KEY ("id")
)
WITH (
    OIDS = FALSE
);
 