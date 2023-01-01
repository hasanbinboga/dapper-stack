CREATE TABLE IF NOT EXISTS public.newoffices
(
		"id" bigserial NOT NULL,
    office_code character varying(10) COLLATE pg_catalog."default" NOT NULL,
    city character varying(50) COLLATE pg_catalog."default" NOT NULL,
    phone character varying(50) COLLATE pg_catalog."default" NOT NULL,
    address_line1 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    address_line2 character varying(50) COLLATE pg_catalog."default" DEFAULT NULL::character varying,
    state character varying(50) COLLATE pg_catalog."default" DEFAULT NULL::character varying,
    country character varying(50) COLLATE pg_catalog."default" NOT NULL,
    postal_code character varying(15) COLLATE pg_catalog."default" NOT NULL,
    territory character varying(10) COLLATE pg_catalog."default" NOT NULL,
		is_deleted boolean default false,
		create_time timestamp NOT NULL,
		update_time timestamp,
		create_ip_address inet NOT NULL,
		update_ip_address inet,
		create_user_name character varying(255) NOT NULL,
		update_user_name character varying(255),
		history jsonb,
	  CONSTRAINT newoffices_pkey PRIMARY KEY ("id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;