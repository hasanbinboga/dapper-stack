CREATE TABLE IF NOT EXISTS public.neworders
(
	  "id" bigserial NOT NULL,
    customer_ref bigint NOT NULL, 
    order_date date NOT NULL,
    required_date date NOT NULL,
    shipped_date date,
    status character varying(15) COLLATE pg_catalog."default" NOT NULL,
    comments text COLLATE pg_catalog."default",
    is_deleted boolean default false,
		create_time timestamp NOT NULL,
		update_time timestamp,
		create_ip_address inet NOT NULL,
		update_ip_address inet,
		create_user_name character varying(255) NOT NULL,
		update_user_name character varying(255),
		history jsonb,
	  CONSTRAINT neworders_pkey PRIMARY KEY (id),
    CONSTRAINT neworders_customer_ref_fkey FOREIGN KEY (customer_ref)
        REFERENCES public.newcustomers (id) MATCH SIMPLE
        ON UPDATE RESTRICT
        ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;