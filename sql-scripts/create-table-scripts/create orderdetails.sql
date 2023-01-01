CREATE TABLE IF NOT EXISTS public.neworderdetails
(
	  "id" bigserial NOT NULL,
    order_ref bigint NOT NULL,
    product_ref bigint NOT NULL,
    quantity_ordered integer NOT NULL,
    price_each numeric(10,2) NOT NULL,
    order_line_number smallint NOT NULL,
		is_deleted boolean default false,
		create_time timestamp NOT NULL,
		update_time timestamp,
		create_ip_address inet NOT NULL,
		update_ip_address inet,
		create_user_name character varying(255) NOT NULL,
		update_user_name character varying(255),
		history jsonb,
	  CONSTRAINT neworderdetails_pkey PRIMARY KEY (id),
    CONSTRAINT neworderdetails_order_ref_fkey FOREIGN KEY (order_ref)
        REFERENCES public.neworders (id) MATCH SIMPLE
        ON UPDATE RESTRICT
        ON DELETE RESTRICT,
    CONSTRAINT neworderdetails_product_ref_fkey FOREIGN KEY (product_ref)
        REFERENCES public.newproducts (id) MATCH SIMPLE
        ON UPDATE RESTRICT
        ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;