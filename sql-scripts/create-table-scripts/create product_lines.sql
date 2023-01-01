CREATE TABLE IF NOT EXISTS public.newproduct_lines
(
	  "id" bigserial NOT NULL,
    product_line character varying(50) COLLATE pg_catalog."default" NOT NULL,
    text_description character varying(4000) COLLATE pg_catalog."default" DEFAULT NULL::character varying,
    html_description text COLLATE pg_catalog."default",
    image bytea,
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
)
TABLESPACE pg_default;