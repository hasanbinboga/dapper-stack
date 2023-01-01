CREATE TABLE IF NOT EXISTS public.newpayments
(
	  "id" bigserial NOT NULL,
    customer_ref bigint NOT NULL,
    check_number character varying(50) COLLATE pg_catalog."default" NOT NULL,
    payment_date date NOT NULL,
    amount numeric(10,2) NOT NULL,
		is_deleted boolean default false,
		create_time timestamp NOT NULL,
		update_time timestamp,
		create_ip_address inet NOT NULL,
		update_ip_address inet,
		create_user_name character varying(255) NOT NULL,
		update_user_name character varying(255),
		history jsonb,
	  CONSTRAINT newpayments_pkey PRIMARY KEY (id),
    CONSTRAINT newpayments_customer_ref_fkey FOREIGN KEY (customer_ref)
        REFERENCES public.newcustomers (id) MATCH SIMPLE
        ON UPDATE RESTRICT
        ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;