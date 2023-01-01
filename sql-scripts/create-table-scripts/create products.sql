CREATE TABLE IF NOT EXISTS public.newproducts
(
	  "id" bigserial NOT NULL,
		product_line_ref bigint NOT NULL,
    product_code character varying(15) COLLATE pg_catalog."default" NOT NULL,
    product_name character varying(70) COLLATE pg_catalog."default" NOT NULL,
    product_line character varying(50) COLLATE pg_catalog."default" NOT NULL,
    product_scale character varying(10) COLLATE pg_catalog."default" NOT NULL,
    product_vendor character varying(50) COLLATE pg_catalog."default" NOT NULL,
    product_description text COLLATE pg_catalog."default" NOT NULL,
    quantity_in_stock smallint NOT NULL,
    buy_price numeric(10,2) NOT NULL,
    msrp numeric(10,2) NOT NULL,
		is_deleted boolean default false,
		create_time timestamp NOT NULL,
		update_time timestamp,
		create_ip_address inet NOT NULL,
		update_ip_address inet,
		create_user_name character varying(255) NOT NULL,
		update_user_name character varying(255),
		history jsonb,
	  CONSTRAINT newproducts_pkey PRIMARY KEY (id),
    CONSTRAINT newproducts_product_line_ref_fkey FOREIGN KEY (product_line_ref)
        REFERENCES public.newproduct_lines (id) MATCH SIMPLE
        ON UPDATE RESTRICT
        ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;