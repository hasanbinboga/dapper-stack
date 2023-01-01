CREATE TABLE IF NOT EXISTS public.newemployees
(
		"id" bigserial NOT NULL,
		office_ref bigint NOT NULL,
    reports_to bigint,
    first_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    last_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    extension character varying(10) COLLATE pg_catalog."default" NOT NULL,
    email character varying(100) COLLATE pg_catalog."default" NOT NULL,    
    job_title character varying(50) COLLATE pg_catalog."default" NOT NULL,
		is_deleted boolean default false,
		create_time timestamp NOT NULL,
		update_time timestamp,
		create_ip_address inet NOT NULL,
		update_ip_address inet,
		create_user_name character varying(255) NOT NULL,
		update_user_name character varying(255),
		history jsonb,
	  CONSTRAINT newemployees_pkey PRIMARY KEY (id),
    CONSTRAINT newemployees_office_ref_fkey FOREIGN KEY (office_ref)
        REFERENCES public.newoffices (id) MATCH SIMPLE
        ON UPDATE RESTRICT
        ON DELETE RESTRICT,
    CONSTRAINT newemployees_reports_to_fkey FOREIGN KEY (reports_to)
        REFERENCES public.newemployees (id) MATCH SIMPLE
        ON UPDATE RESTRICT
        ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default; 
 